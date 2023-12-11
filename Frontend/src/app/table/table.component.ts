import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {ApiService} from "../services/api.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort, Sort} from "@angular/material/sort";
import {MatPaginator} from "@angular/material/paginator";
import {SensorGroupCheckBox} from "../models/checkboxes";
import {MatCheckboxChange} from "@angular/material/checkbox";
import {FilterRequest, SortRequest} from "../models/requests";
import {Client, SensorMeasurementDto, SortDirection} from "../services/services";

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements AfterViewInit {

  queryPageSize = 10;
  dataSize?: number;
  dataSource = new MatTableDataSource<SensorMeasurementDto>();
  displayedColumns = ['Sensor Type', 'Sensor Name' , 'Value', 'Date'];

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

  private sortRequest: SortRequest = {
    columnName: "Date",
    direction: SortDirection._0
  }
  private filterRequest: FilterRequest = {
    from: undefined,
    to: undefined,
    sensorType: this.getCheckedSensorTypes(),
    sensorName: this.getCheckedSensorNames()
  }

  constructor (
    private readonly apiService: ApiService,
    private readonly client: Client
  ) {  }

  allComplete: boolean[] = [true, true];

  updateAllComplete(i: number, name: string, $event: MatCheckboxChange) {
    this.sensorGroup[i].sensors.forEach(s => {
      if (s.name == name)
        s.checked = $event.checked;
    })
    this.allComplete[i] = this.sensorGroup[i].sensors != null && this.sensorGroup[i].sensors.every(t => t.checked);
    this.filterRequest.sensorName = this.getCheckedSensorNames();
    this.loadData();
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
    this.filterRequest.sensorType = this.getCheckedSensorTypes();
    this.loadData();
  }

  ngAfterViewInit(): void {
    this.loadData();
  }

  sortColumnChanged($event: Sort) {
    this.sortRequest = {columnName: $event.active, direction: $event.direction == "asc" ? SortDirection._0 : SortDirection._1}
    this.loadData()
  }

  private loadData() {
    return this.client.sensors(
      this.filterRequest.from, this.filterRequest.to, this.filterRequest.sensorType,
      this.filterRequest.sensorName, this.sortRequest.columnName, this.sortRequest.direction)
      .subscribe(data => {
        this.dataSource.data = data;
        this.dataSize = data.length;
      })
  }

  private getCheckedSensorTypes() {
    return this.sensorGroup.filter(g => g.checked).map(t => t.name);
  }

  private getCheckedSensorNames() {
    const checked = this.sensorGroup.map(group =>
      group.sensors.filter(s => s.checked).map(s => s.name) ?? []
    )
    return checked.reduce((acc, curr) => acc.concat(curr), []);
  }
}
