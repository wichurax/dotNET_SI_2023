import {SortDirection} from "../services/services";

export interface FilterRequest {
  from?: Date
  to?: Date
  sensorType: string[]
  sensorName: string[]
}

export interface SortRequest {
  columnName: string
  direction: SortDirection._0 | SortDirection._1;
}
