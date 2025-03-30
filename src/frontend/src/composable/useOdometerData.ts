import { ref } from 'vue'
import { OdometerAPI, type OdometerFilterParamsInput, type OdometerTrackerResponse } from '../api/odometer'
import type { ValidationErrorResponseList } from '../api'

export function useOdometerData() {
  const odometerData = ref<OdometerTrackerResponse>()
  const loading = ref(false)
  const error = ref<ValidationErrorResponseList>([])

  async function fetchOdometerData(filters: OdometerFilterParamsInput) {
    loading.value = true
    const data = await OdometerAPI.fetchOdometerTrackerData(filters)
    loading.value = false

    if (data.isError) {
      error.value = data.data as ValidationErrorResponseList
      return;
    }

    odometerData.value = data.data as OdometerTrackerResponse
  }

  return { odometerData, loading, error, fetchOdometerData }
}
