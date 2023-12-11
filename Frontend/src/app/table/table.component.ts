import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {Sort} from "@angular/material/sort";
import {MatPaginator} from "@angular/material/paginator";
import {SensorGroupCheckBox} from "../models/checkboxes";
import {MatCheckboxChange} from "@angular/material/checkbox";
import {FilterRequest, SortRequest} from "../models/requests";
import {Client, SensorMeasurementDto, SortDirection} from "../services/services";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements AfterViewInit {

  queryPageSize = 10;
  dataSize?: number;
  dataSource = new MatTableDataSource<SensorMeasurementDto>();
  displayedColumns = ['SensorType', 'SensorName' , 'Value', 'MeasurementDate'];

  @ViewChild(MatPaginator) paginator?: MatPaginator;

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

  dateRange = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  private sortRequest: SortRequest = {
    columnName: "MeasurementDate",
    direction: SortDirection._0
  }
  private filterRequest: FilterRequest = {
    from: undefined,
    to: undefined,
    sensorType: this.getCheckedSensorTypes(),
    sensorName: this.getCheckedSensorNames()
  }

  constructor (
    private readonly client: Client
  ) {  }

  allComplete: boolean[] = [true, true];

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator!;
    this.loadData();
  }

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

  sortColumnChanged($event: Sort) {
    this.sortRequest = {columnName: $event.active, direction: $event.direction == "asc" ? SortDirection._0 : SortDirection._1}
    this.loadData()
  }

  dateChanged() {
    this.filterRequest.from = this.dateRange.controls.start.value ?? undefined;
    this.filterRequest.to = this.dateRange.controls.end.value ?? undefined;
    this.loadData();
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

  private loadData() {
    return this.client.sensors(
      this.filterRequest.from, this.filterRequest.to, this.filterRequest.sensorType,
      this.filterRequest.sensorName, this.sortRequest.columnName, this.sortRequest.direction)
      .subscribe(data => {
        this.dataSource.data = data;
        this.dataSize = data.length;
      })
  }
}
