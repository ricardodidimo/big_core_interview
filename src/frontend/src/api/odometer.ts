import type { AxiosResponse } from "axios";
import api, {
  DefaultErrorServerResponse,
  GenerateApiResponse,
  type ApiResponse,
  type ValidationErrorResponseList,
} from "./index";
import axios from "axios";

export interface OdometerFilterParamsInput {
  StartDate: string;
  EndDate: string;
  IdTms?: string[];
  LicensePlate?: string[];
  DivisionId?: number[];
  Rows: number;
  Page: number;
}

export interface OdometerTrackerResponse {
  data: OdometerTrackerData[];
  totalItems: number;
  numberOfRowPage: number;
  pageActive: number;
  totalPages: number;
}

export enum VehicleStatus {
  MOVING = 0,
  STOPPED = 1,
  DELAYED_MOV = 2,
  DELAYED_STOPPED = 3
}

export interface OdometerTrackerData {
  vehicleId?: number;
  vehicleIdTms?: string;
  operationId?: number;
  operationName?: string;
  divisionId?: number;
  divisionName?: string;
  licensePlate?: string;
  odometerKm?: number;
  speed?: number;
  vehicleStatus: VehicleStatus;
  ignition: boolean;
  driverId?: number;
  driverName?: string;
  dateProcess?: string;
}

export const OdometerAPI = {
  async fetchOdometerTrackerData(
    filters: OdometerFilterParamsInput
  ): Promise<ApiResponse<OdometerTrackerResponse | ValidationErrorResponseList>> {
    try {
      const response: AxiosResponse<OdometerTrackerResponse> = await api.get(
        "/odometer/tracker",
        {
          params: filters,
        }
      );

      console.log(response)
      return GenerateApiResponse<OdometerTrackerResponse>(response);
    } catch (err) {
      if (axios.isAxiosError(err) && err.response) {
        return GenerateApiResponse<ValidationErrorResponseList>(err.response);
      }

      return DefaultErrorServerResponse;
    }
  },
};
