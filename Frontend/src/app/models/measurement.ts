export interface Measurement {
  type: 'Temperature' | 'Pressure'
  name: string
  value: number
  unit: string
  timestamp: Date
}
