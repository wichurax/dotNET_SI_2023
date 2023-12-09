import {ThemePalette} from "@angular/material/core";

export interface SensorGroupCheckBox {
  name: string
  checked: boolean
  color: ThemePalette
  sensors: SensorCheckBox[]
}

export interface SensorCheckBox {
  name: string
  checked: boolean
  color: ThemePalette
}
