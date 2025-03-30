<script setup lang="ts">
import { defineProps, defineEmits, ref } from 'vue';
import { useI18n } from 'vue-i18n';
import type { OdometerFilterParamsInput } from '../../api/odometer';
import MultiEntryInput from '../MultiEntryInput.vue';
import { fromLocalToUTC, getLocalDatesIntervals } from '../../helpers/dateFormatter';

const { t } = useI18n();

const props = defineProps<{
    filters: OdometerFilterParamsInput
}>();

const { startDate, endDate } = getLocalDatesIntervals();
const tempFilters = ref<OdometerFilterParamsInput>({ ...props.filters, StartDate: startDate, EndDate: endDate });
const isOpen = ref(false);
const emit = defineEmits(['applyFilters']);

const applyFilters = () => {
    const { startDate, endDate } = fromLocalToUTC(tempFilters.value.StartDate, tempFilters.value.EndDate);
    tempFilters.value.StartDate = startDate;
    tempFilters.value.EndDate = endDate;
    emit('applyFilters', tempFilters.value);
    isOpen.value = false;
};

const allDivisions: { id: number, name: string }[] = [
    { id: 39, name: "Citrosuco" },
    { id: 42, name: "GLP" },
    { id: 45, name: "Amônia" },
    { id: 46, name: "Máquinas" },
    { id: 55, name: "Ácido" },
    { id: 58, name: "Treinamento" }
];

function removeItem(index: number) {
    const updatedContainer = [...tempFilters.value.DivisionIds!];
    updatedContainer.splice(index, 1);

    tempFilters.value.DivisionIds = updatedContainer
}
</script>

<template>
    <v-icon icon="fa fa-filter" @click="isOpen = true"></v-icon>
    <v-dialog v-model="isOpen" max-width="500px">
        <v-card>
            <v-card-title class="d-flex justify-space-between align-center">
                <span>{{ t('configure_filters_modal.modal_title') }}</span>
                <v-btn icon="fa fa-close" variant="text" :rounded="false" @click="isOpen = false"></v-btn>
            </v-card-title>

            <v-card-text>
                <div class="my-4">
                    <span>{{ t('configure_filters_modal.date_interval') }}</span>
                    <div class="d-flex">
                        <v-text-field v-model="tempFilters.StartDate" class="mr-2"
                            :label="t('configure_filters_modal.start_date')" type="datetime-local" dense></v-text-field>
                        <v-text-field v-model="tempFilters.EndDate" :label="t('configure_filters_modal.end_date') "
                            type="datetime-local" dense></v-text-field>
                    </div>
                </div>

                <div class="my-4">
                    <MultiEntryInput :container="tempFilters.IdTms!" class="my-5"
                        :label="t('configure_filters_modal.fleet')"
                        @update:container="(val) => tempFilters.IdTms = val" />
                    <MultiEntryInput :container="tempFilters.LicensePlates!"
                        :label="t('configure_filters_modal.license_plate')"
                        @update:container="(val) => tempFilters.LicensePlates = val" />
                </div>

                <div class="my-4">
                    <span>{{ t('configure_filters_modal.division') }}</span>
                    <v-select v-model="tempFilters.DivisionIds" :item-title="(d) => `${d.id} | ${d.name}`"
                        item-value="id" :items="allDivisions" :label="t('configure_filters_modal.select_divisions')"
                        multiple dense clearable></v-select>

                    <v-chip-group v-if="tempFilters.DivisionIds?.length" prev-icon="fa fa-arrow-left"
                        next-icon="fa fa-arrow-right" show-arrows>
                        <v-chip v-for="(item, index) in tempFilters.DivisionIds" :key="index"
                            @click.stop="removeItem(index)">
                            {{allDivisions.find(d => d.id === item)?.name}}
                        </v-chip>
                    </v-chip-group>
                </div>

                <div class="d-flex justify-end mt-3">
                    <v-btn color="primary" @click="applyFilters">{{ t('configure_filters_modal.ok_button')
                        }}</v-btn>
                </div>
            </v-card-text>
        </v-card>
    </v-dialog>
</template>

<style scoped></style>
