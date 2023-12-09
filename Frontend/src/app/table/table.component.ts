import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {ApiService} from "../services/api.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort, Sort} from "@angular/material/sort";
import {MatPaginator} from "@angular/material/paginator";
import {Measurement} from "../models/measurement";
import {SensorGroupCheckBox} from "../models/checkboxes";
import {MatCheckboxChange} from "@angular/material/checkbox";

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements AfterViewInit {

  queryPageSize = 10;
  dataSize?: number;
  dataSource = new MatTableDataSource<Measurement>();
  displayedColumns = ['Sensor Type', 'Sensor Name' , 'Value', 'Unit', 'Timestamp'];

  @ViewChild(MatPaginator) paginator?: MatPaginator;
  @ViewChild(MatSort) sort?: MatSort;

  /* checkboxes */
  sensorGroup: SensorGroupCheckBox[] = [{
    name: 'Temperature',
    checked: true,
    color: 'accent',
    sensors: [
      {name: 'T01', checked: true, color: 'primary'},
      {name: 'T02', checked: true, color: 'primary'},
      {name: 'T03', checked: true, color: 'primary'},
      {name: 'T04', checked: true, color: 'primary'},
    ],
  },
    {
      name: 'Pressure',
      checked: true,
      color: 'accent',
      sensors: [
        {name: 'P01', checked: true, color: 'primary'},
        {name: 'P02', checked: true, color: 'primary'},
        {name: 'P03', checked: true, color: 'primary'},
        {name: 'P04', checked: true, color: 'primary'},
      ],
    }];

  constructor (
    private readonly apiService: ApiService
  ) {  }

  allComplete: boolean[] = [true, true];

  updateAllComplete(i: number, name: string, $event: MatCheckboxChange) {
    this.sensorGroup[i].sensors.forEach(s => {
      if (s.name == name)
        s.checked = $event.checked;
    })
    this.allComplete[i] = this.sensorGroup[i].sensors != null && this.sensorGroup[i].sensors.every(t => t.checked);
    this.filterData();
  }

  someComplete(i: number): boolean {
    if (this.sensorGroup[i].sensors == null) {
      return false;
    }
    return this.sensorGroup[i].sensors.filter(t => t.checked).length > 0 && !this.allComplete[i];
  }

  setAll(i: number, $event: MatCheckboxChange) {
    this.allComplete[i] = $event.checked;
    if (this.sensorGroup[i].sensors == null) {
      return;
    }
    this.sensorGroup[i].checked = $event.checked;
    this.sensorGroup[i].sensors.forEach(t => (t.checked = $event.checked));
    this.filterData();
  }

  ngAfterViewInit(): void {
    this.apiService.getTemperature(20)
      .subscribe(data => {
        this.dataSource.data = data;
        this.dataSource.sort = this.sort!;
        this.dataSource.paginator = this.paginator!;
      });
    this.apiService.getPressure(20)
      .subscribe(data => {
        this.dataSource.data.push(...data);
        this.dataSize = this.dataSource.data.length;
      });
    this.dataSource.filterPredicate = (data, filter) => filter.split(',').includes(data.name);
    this.filterData();
  }

  private filterData() {
    const checked = this.sensorGroup.map(group =>
      group.sensors.filter(s => s.checked).map(s => s.name) ?? []
    )
    const merged = checked.reduce((acc, curr) => acc.concat(curr), []);
    this.dataSource.filter = merged.join(',');
  }

  sortColumnChanged($event: Sort) {
    const dir = $event.direction == 'asc' ? 1 : -1
    switch ($event.active) {
      case 'Sensor Type':
        this.dataSource.filteredData.sort((a, b) => a.type.localeCompare(b.type) * dir);
        break;
      case 'Sensor Name':
        this.dataSource.filteredData.sort((a, b) => a.name.localeCompare(b.name) * dir);
        break;
      case 'Value':
        this.dataSource.filteredData.sort((a, b) => (a.value < b.value ? -1 : 1) * dir);
        break;
      case 'Unit':
        this.dataSource.filteredData.sort((a, b) => a.unit.localeCompare(b.unit) * dir);
        break;
      default:
        this.dataSource.filteredData.sort((a, b) => (a.timestamp < b.timestamp ? -1 : 1) * dir);
        break;
    }
  }
}
