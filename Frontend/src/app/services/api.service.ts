import { Injectable } from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {Measurement} from "../models/measurement";

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor() { }

  // wymagania: 4 typy czujnikow po 4 instancje

  // TODO http requests to backend instead of custom data

  private generateData(type: 'Temperature' | 'Pressure', size: number, unit: string): Measurement[] {
    const result: Measurement[] = [];
    const name = type == 'Temperature' ? 'T0' : 'P0'
    for (let _ = 0; _ < size; _++) {
      const temperatureEntry: Measurement = {
        type: type,
        name: name + this.getRandomInteger(1, 4),
        value: this.getRandomFloat(17.0, 22.0),
        unit: unit,
        timestamp: new Date()
      };

      result.push(temperatureEntry);
    }

    return result;
  }

  private getRandomInteger(min: number, max: number): number {
    return Math.round(Math.random() * (max - min) + min);
  }

  private getRandomFloat(min: number, max: number): number {
    return Math.random() * (max - min) + min;
  }

  private temperatureSubject = new BehaviorSubject<Measurement[]>([]);
  private pressureSubject = new BehaviorSubject<Measurement[]>([]);

  getTemperature(size: number) {
    this.temperatureSubject.next(this.generateData('Temperature', size, 'Celsius'));
    return this.temperatureSubject.asObservable();
  }

  getPressure(size: number) {
    this.pressureSubject.next(this.generateData('Pressure', size, 'hPa'));
    return this.pressureSubject.asObservable();
  }
}
