<script setup lang="ts">
import { translateVehicleStatus, type OdometerTrackerData } from '../../api/odometer';
import { ref, defineEmits, onMounted, computed, nextTick } from 'vue';
import { useI18n } from 'vue-i18n'
import { getTablePreferences, setTablePreferences } from '../../helpers/localStoragePersistence';
const { t, locale } = useI18n();

const isOpen = ref(false);
const emit = defineEmits(['update-fields']);
const closeModal = () => isOpen.value = false;

onMounted(() => {
    const configPreferences = getTablePreferences();
    if (configPreferences != null) {
        tableFields.value = configPreferences.map<OdometerTrackerTableFields>(field => ({
            ...field,
            body: tableFields.value.find(f => f.key === field.key)!.body!
        }));
    }

    updateFields();
})

const updateFields = () => {
    setTablePreferences(tableFields.value);
    emit('update-fields', tableFields.value.filter(item => item.visible));
};

export interface OdometerTrackerTableFields {
    key: string,
    visible: boolean,
    header: string,
    body: (key: OdometerTrackerData) => Object | null
};

const tableFields = ref<OdometerTrackerTableFields[]>([
    { key: 'fleet', visible: true, header: 'odometer_table_header.fleet', body: (item: any) => item.vehicleIdTms },
    { key: 'operation', visible: true, header: 'odometer_table_header.operation', body: (item: any) => item.operationName },
    { key: 'division', visible: true, header: 'odometer_table_header.division', body: (item: any) => item.divisionName },
    { key: 'licensePlate', visible: true, header: 'odometer_table_header.license_plate', body: (item: any) => item.licensePlate },
    { key: 'odometerKm', visible: true, header: 'odometer_table_header.odometer_km', body: (item: any) => item.odometerKm },
    { key: 'speed', visible: true, header: 'odometer_table_header.speed', body: (item: any) => `${item.speed} km/h` },
    { key: 'vehicleStatus', visible: true, header: 'odometer_table_header.vehicle_status', body: (item: OdometerTrackerData) => translateVehicleStatus(item.vehicleStatus, t) },
    {
        key: 'ignitionStatus',
        visible: true,
        header: 'odometer_table_header.ignition_status',
        body: (item: any) => item.ignition
            ? t('odometer_table_body.ignition_status_on')
            : t('odometer_table_body.ignition_status_off')
    },
    { key: 'driver', visible: true, header: 'odometer_table_header.driver', body: (item: any) => item.driverName },
    {
        key: 'date',
        visible: true,
        header: 'odometer_table_header.date',
        body: (item: any) => item.dateProcess
            ? new Date(item.dateProcess).toLocaleString(locale.value, {
                year: 'numeric', month: '2-digit', day: '2-digit',
                hour: '2-digit', minute: '2-digit'
            })
            : null
    }
]);

const fieldsToPick = computed(() => tableFields.value.filter(field => !field.visible))
const fieldsPicked = computed(() => tableFields.value.filter(field => field.visible))
const selectedField = ref<OdometerTrackerTableFields | null>(null);
const changeFieldVisibility = (field: OdometerTrackerTableFields) => {
    const fieldToUpdate = tableFields.value.find(f => f.key === field.key);
    if (fieldToUpdate) {
        fieldToUpdate.visible = true;
    }
    selectedField.value = null;
};

async function swapPositions(direction: 'up' | 'down', key: string) {
    const idx = tableFields.value.findIndex(i => i.key === key);
    if (direction === 'up' && idx <= 0) { await nextTick(); return; };
    if (direction === 'down' && idx >= fieldsPicked.value.length) { await nextTick(); return; };

    let targetIdx = direction === 'up' ? idx - 1 : idx + 1;
    while (tableFields.value[targetIdx].visible == false) {
        targetIdx = direction === 'up' ? targetIdx - 1 : targetIdx + 1;
    }

    [tableFields.value[idx], tableFields.value[targetIdx]] =
        [tableFields.value[targetIdx], tableFields.value[idx]];
    await nextTick();
}
</script>

<template>
    <div>
        <v-icon icon="fa fa-gear" @click="isOpen = true"></v-icon>

        <v-dialog v-model="isOpen" max-width="500px">
            <v-card>
                <v-card-title class="d-flex justify-space-between align-center">
                    <span>{{ t('configure_visualization_modal.title') }}</span>
                    <v-btn icon="fa fa-close" rounded="0" variant="text" @click="closeModal"></v-btn>
                </v-card-title>
                <v-card-text>

                    <v-select v-model="selectedField" :items="fieldsToPick" :item-title="(i) => t(i.header)" return-object
                        :label="t('configure_visualization_modal.pick_column_label')" dense :hide-details="true"
                        density="compact" @update:modelValue="changeFieldVisibility" />

                    <div>

                        <v-data-table :items="fieldsToPick" dense hide-default-footer>
                            <template v-slot:headers>
                                <tr>
                                    <th>
                                        {{ t('configure_visualization_modal.visible_columns_title') }}
                                    </th>
                                </tr>
                            </template>

                            <template v-slot:body>
                                <tr v-for="(item, index) in fieldsPicked" :key="item.key">
                                    <td>{{ t(item.header) }}</td>
                                    <td class="d-flex align-center justify-end">
                                        <v-icon icon="fa fa-arrow-down" size="15" class="mx-1"
                                            @click="() => swapPositions('down', item.key)"
                                            :disabled="fieldsPicked.length <= 1 || index >= fieldsPicked.length - 1" />
                                        <v-icon icon="fa fa-minus" size="15" class="mx-1"
                                            @click="() => item.visible = !item.visible"
                                            :disabled="fieldsPicked.length <= 1" />
                                        <v-icon icon="fa fa-arrow-up" class="mx-1" size="15"
                                            @click="() => swapPositions('up', item.key)"
                                            :disabled="fieldsPicked.length <= 1 || index <= 0" />
                                    </td>
                                </tr>
                            </template>
                        </v-data-table>

                    </div>
                </v-card-text>

                <v-card-actions class="d-flex justify-end">
                    <v-btn variant="text" @click="closeModal">{{ t('configure_visualization_modal.cancel_btn')
                        }}</v-btn>
                    <v-btn color="text" @click="updateFields">{{ t('configure_visualization_modal.save_btn') }}</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>

</template>

<style scoped></style>