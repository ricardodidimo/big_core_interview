<script setup lang="ts">
import { ref, watch } from 'vue'
import { useOdometerData } from '../composable/useOdometerData'
import { type OdometerFilterParamsInput } from '../api/odometer'
import { useI18n } from 'vue-i18n'
import ConfigureVisualizationModal, { type OdometerTrackerTableFields } from './odometerTrackerTable/ConfigureVisualizationModal.vue';
import ConfigureFiltersModal from './odometerTrackerTable/ConfigureFiltersModal.vue';
import { getUTCDatesIntervals } from '../helpers/dateFormatter';
import { getFilters, setFilters } from '../helpers/localStoragePersistence';

const { t } = useI18n();
const { odometerData, loading, error, fetchOdometerData } = useOdometerData()

const { startDate, endDate } = getUTCDatesIntervals();
const filters = ref<OdometerFilterParamsInput>(getFilters() ?? {
    StartDate: startDate,
    EndDate: endDate,
    Rows: 10,
    Page: 1,
    IdTms: [],
    LicensePlates: []
});

function applyFilters(filtersUpdated: OdometerFilterParamsInput) {
    filters.value = filtersUpdated
}

watch(filters, async () => {
    await fetchOdometerData(filters.value);
    if (error.value.length <= 0) {
        setFilters(filters.value)
    }
}, { immediate: true, deep: true });

const tableFields = ref<OdometerTrackerTableFields[]>();
function updateFieldsVisualization(newVisualization: OdometerTrackerTableFields[]) {
    tableFields.value = newVisualization;
}

function checkForReplacement(result?: Object | null) {
    const replacement = "-";
    if (result == null || result == undefined) return replacement;
    if (typeof (result) == "string") return result == "" ? replacement : result

    return result
}
</script>

<template>
    <v-container>
        <div v-for="(error) in error" class="text-start">
            <v-alert v-if="error.reasons.length <= 0">
                <p>{{ t(`errors.${error.message}`) }}</p>
            </v-alert>
            <v-alert type="error" density="compact">
                <v-icon icon="fa fa-xmark" />
                {{  t(`errors.validation_failed`) + t(`fields.${error.metadata.PropertyName}`) }}</v-alert>
            <ul class="px-10">
                <li v-for="(reason) in error.reasons">{{ t(`errors.${reason.message}`) }}</li>
            </ul>
        </div>

        <div v-if="!loading">
            <div class="w-100 d-flex justify-space-between align-center">
                <div class="d-flex">
                    <p class="mr-1"> {{ t('odometer_table_actions.action') }}</p>
                    <ConfigureFiltersModal :filters="filters" @apply-filters="applyFilters" />
                </div>
                <div class="my-2 d-flex justify-end align-center">
                    <v-select v-model="filters.Rows" :items="[10, 20, 50]"
                        :label="t('odometer_table_actions.records_per_page_label')" dense class="page-select"
                        :hide-details="true" density="compact" />
                    <v-pagination v-model="filters.Page" :size="35" prev-icon="fa fa-chevron-left"
                        next-icon="fa fa-chevron-right" :length="odometerData?.totalPages" :total-visible="5" />
                    <ConfigureVisualizationModal @update-fields="updateFieldsVisualization" class="ml-4" />
                </div>
            </div>

            <v-data-table :items="odometerData?.data" :items-per-page="filters.Rows" class="table" dense
                hide-default-footer>
                <template v-slot:headers>
                    <tr>
                        <th class="text-center" v-for="(field, index) in tableFields" :key="index">
                            {{ t(field.header) }}
                        </th>
                    </tr>
                </template>

                <template v-slot:body>
                    <tr v-for="item in odometerData?.data" :key="item.vehicleId">
                        <td v-for="(field, index) in tableFields" :key="index">{{
                            checkForReplacement(field.body(item)) }}</td>
                    </tr>
                </template>
            </v-data-table>

        </div>
        <div v-else>

            <div class="my-2 d-flex justify-space-between align-center">
                <div>
                    <v-skeleton-loader class="page-select-skeleton" />
                </div>
                
                <div class="pagination-skeleton d-flex align-center">
                    <v-skeleton-loader class="page-select-skeleton" />
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
    min-width: 150px;
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