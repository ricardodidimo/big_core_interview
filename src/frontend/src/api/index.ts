import axios, { type AxiosResponse } from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json",
  },
  responseType: "json",
});


interface ValidationErrorReason {
  reasons: object[];
  message: string;
  metadata: object[]
}

interface ValidationErrorResponse {
  reasons: ValidationErrorReason[];
  message: string;
  metadata: {
    PropertyName: string;
  };
}

export type ValidationErrorResponseList = ValidationErrorResponse[];

export interface ApiResponse<T> {
  data: T | ValidationErrorResponseList;
  isError: boolean;
}

export function GenerateApiResponse<T>(response: AxiosResponse<T>): ApiResponse<T|ValidationErrorResponseList> {
  return {
    data: response.data,
    isError: response.status >= 400
  }
}

export const DefaultErrorServerResponse: ApiResponse<ValidationErrorResponseList> = {
  data: [{
    message: "Unknown error occurred",
    metadata: {
      PropertyName: "Server",
    },
    reasons: [{
      message: "Unknown error occurred",
      metadata: [],
      reasons: []
    }],
  }],
  isError: true,
}

export default api;
