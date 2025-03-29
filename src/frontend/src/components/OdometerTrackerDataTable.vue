<script setup lang="ts">
import { ref, watch } from 'vue'
import { useOdometerData } from '../composable/useOdometerData'
import { translateVehicleStatus, type OdometerFilterParamsInput } from '../api/odometer'
import { useI18n } from 'vue-i18n'

const { t, locale } = useI18n();
const { odometerData, loading, error, fetchOdometerData } = useOdometerData()

const filters = ref<OdometerFilterParamsInput>({
    StartDate: '2025-03-23T17:08Z',
    EndDate: '2025-03-24T17:08Z',
    Rows: 10,
    Page: 1
});

watch(filters, () => {
    fetchOdometerData(filters.value);
}, { immediate: true, deep: true });
</script>

<template>
    <v-container>
        <v-alert v-if="error" type="error">{{ error }}</v-alert>

        <div v-if="!loading && !error">
            <div class="my-2 d-flex justify-end align-center">
                <v-select v-model="filters.Rows" :items="[10, 20, 50]"
                    :label="t('odometer_table_actions.records_per_page_label')" dense class="page-select"
                    :hide-details="true" density="compact" />
                <v-pagination v-model="filters.Page" :size="40" prev-icon="fa fa-chevron-left"
                    next-icon="fa fa-chevron-right" :length="odometerData?.totalPages" :total-visible="5" />
            </div>

            <v-data-table :items="odometerData?.data" :items-per-page="filters.Rows" class="table" dense hide-default-footer>
                <template v-slot:headers>
                    <tr>
                        <th>{{ $t('odometer_table_header.fleet') }}</th>
                        <th>{{ $t('odometer_table_header.operation') }}</th>
                        <th>{{ $t('odometer_table_header.division') }}</th>
                        <th>{{ $t('odometer_table_header.license_plate') }}</th>
                        <th>{{ $t('odometer_table_header.odometer_km') }}</th>
                        <th>{{ $t('odometer_table_header.speed') }}</th>
                        <th>{{ $t('odometer_table_header.vehicle_status') }}</th>
                        <th>{{ $t('odometer_table_header.ignition_status') }}</th>
                        <th>{{ $t('odometer_table_header.driver') }}</th>
                        <th>{{ $t('odometer_table_header.date') }}</th>
                    </tr>
                </template>

                <template v-slot:body>
                    <tr v-for="item in odometerData?.data" :key="item.vehicleId">
                        <td>{{ item.vehicleIdTms ?? "-" }}</td>
                        <td>{{ item.operationName ?? "-" }}</td>
                        <td>{{ item.divisionName ?? "-" }}</td>
                        <td>{{ item.licensePlate ?? "-" }}</td>
                        <td>{{ item.odometerKm ?? "-" }}</td>
                        <td>{{ item.speed ?? "-" }} km/h</td>
                        <td>{{ translateVehicleStatus(item.vehicleStatus, t) }}</td>
                        <td>{{ item.ignition ? $t('odometer_table_body.ignition_status_on') :
                            $t('odometer_table_body.ignition_status_off') }}</td>
                        <td>{{ item.driverName ?? "-" }}</td>
                        <td v-if="item.dateProcess">{{ new Date(item.dateProcess).toLocaleString(locale, {
                            year: 'numeric',
                            month: '2-digit',
                            day: '2-digit',
                            hour: '2-digit',
                            minute: '2-digit'
                        }) }}</td>
                        <td v-else>-</td>
                    </tr>
                </template>
            </v-data-table>

        </div>
        <div v-else>
            <div class="my-2 d-flex justify-end align-center">
                <v-skeleton-loader class="page-select-skeleton" :boilerplate="true" />

                <div class="pagination-skeleton d-flex align-center">
                    <v-skeleton-loader class="pagination-icon" />

                    <v-skeleton-loader v-for="n in 4" :key="n" type="button" class="pagination-number" />
                    <v-skeleton-loader type="text" class="pagination-gap" />
                    <v-skeleton-loader type="button" class="pagination-number" />

                    <v-skeleton-loader class="pagination-icon" />
                </div>
            </div>

            <v-data-table class="table" dense hide-default-footer>
                <template v-slot:headers>
                    <tr>
                        <th v-for="n in 8" :key="n">
                            <v-skeleton-loader type="text" class="loading-header" width="80px"></v-skeleton-loader>
                        </th>
                    </tr>
                </template>
                <template v-slot:body>
                    <tr v-for="n in filters.Rows" :key="n">
                        <td v-for="m in 8" :key="m">
                            <v-skeleton-loader type="text" class="loading" elevation="0"
                                width="100px"></v-skeleton-loader>
                        </td>
                    </tr>
                </template>
            </v-data-table>
        </div>
    </v-container>
</template>

<style lang="scss">
.table {
    background-color: rgb(var(--v-theme-table-bg));
    color: rgb(var(--v-theme-text-color)) !important;
    border: 1px solid rgb(var(--v-theme-border-color));

    th,
    .loading-header {
        background-color: rgb(var(--v-theme-header-bg)) !important;
        color: rgb(var(--v-theme-text-color));
    }

    tr:nth-child(even),
    tr:nth-child(even) .loading {
        background-color: (var(--v-theme-row-even));
    }

    tr:nth-child(odd),
    tr:nth-child(odd) .loading {
        background-color: rgb(var(--v-theme-row-odd));
    }

    tr:hover {
        background-color: rgb(var(--v-theme-hover-bg));
    }
}

.page-select {
    width: auto;
    max-width: 150px;

}

.page-select-skeleton {
    width: 120px;
    height: 40px;
    border-radius: 4px;
    margin-right: 16px;
}

.pagination-skeleton {
    gap: 20px;
}

.pagination-number {
    height: 30px;
}

.pagination-gap {
    height: 10px;
}

.pagination-icon {
    width: 40px;
    height: 40px;
    border-radius: 50%;
}
</style>