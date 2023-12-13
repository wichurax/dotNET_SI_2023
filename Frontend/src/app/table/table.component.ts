import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Sort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { SensorGroupCheckBox } from '../models/checkboxes';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { FilterRequest, SortRequest } from '../models/requests';
import {
  Client,
  FileResponse,
  SensorMeasurementDto,
  SortDirection,
} from '../services/services';
import { FormControl, FormGroup } from '@angular/forms';
import { formatDate } from '@angular/common';
import { HttpClient } from '@angular/common/http';
//import 'rxjs/Rx';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
})
export class TableComponent implements AfterViewInit {
  queryPageSize = 10;
  dataSize?: number;
  dataSource = new MatTableDataSource<SensorMeasurementDto>();
  displayedColumns = ['SensorType', 'SensorName', 'Value', 'MeasurementDate'];

  lineChartData: Array<{ data: number[]; label: string }> = [];
  lineChartLabels: Array<string> = [];
  lineChartOptions: any = {
    responsive: true,
  };
  lineChartLegend = true;
  lineChartType: 'line' = 'line';

  private readonly _backendClient: Client;

  @ViewChild(MatPaginator) paginator?: MatPaginator;

  /* checkboxes */
  sensorGroup: SensorGroupCheckBox[] = [
    {
      name: 'temperature',
      checked: true,
      color: 'accent',
      sensors: [
        { name: 'T0', checked: true, color: 'primary' },
        { name: 'T1', checked: true, color: 'primary' },
        { name: 'T2', checked: true, color: 'primary' },
        { name: 'T3', checked: true, color: 'primary' },
      ],
    },
    {
      name: 'pressure',
      checked: true,
      color: 'accent',
      sensors: [
        { name: 'P4', checked: true, color: 'primary' },
        { name: 'P5', checked: true, color: 'primary' },
        { name: 'P6', checked: true, color: 'primary' },
        { name: 'P7', checked: true, color: 'primary' },
      ],
    },
    {
      name: 'CO2',
      checked: true,
      color: 'accent',
      sensors: [
        { name: 'C8', checked: true, color: 'primary' },
        { name: 'C9', checked: true, color: 'primary' },
        { name: 'C10', checked: true, color: 'primary' },
        { name: 'C11', checked: true, color: 'primary' },
      ],
    },
    {
      name: 'humidity',
      checked: true,
      color: 'accent',
      sensors: [
        { name: 'H12', checked: true, color: 'primary' },
        { name: 'H13', checked: true, color: 'primary' },
        { name: 'H14', checked: true, color: 'primary' },
        { name: 'H15', checked: true, color: 'primary' },
      ],
    },
  ];

  allComplete: boolean[] = [true, true];

  dateRange = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  private sortRequest: SortRequest = {
    columnName: 'MeasurementDate',
    direction: SortDirection._0,
  };

  private filterRequest: FilterRequest = {
    from: undefined,
    to: undefined,
    sensorType: this.getCheckedSensorTypes(),
    sensorName: this.getCheckedSensorNames(),
  };

  constructor(private readonly httpClient: HttpClient) {
    // TODO MS
    //this._backendClient = new Client(httpClient, 'http://localhost:32769');
    this._backendClient = new Client(httpClient, 'http://localhost:32769');
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator!;
    this.loadData();
  }

  private getDaysArray() {
    const dates: number[] = this.dataSource.data.map((m) =>
      m.measurementDate!.getTime()
    );
    const start = Math.min(...dates);
    const end = Math.max(...dates);
    for (
      var arr = [], dt = new Date(start);
      dt <= new Date(end);
      dt.setDate(dt.getDate() + 1)
    ) {
      arr.push(new Date(dt));
    }
    return arr;
  }

  private refreshChart() {
    const sensorNames = this.getCheckedSensorNames();
    this.lineChartLabels = this.getDaysArray().map((d) =>
      formatDate(d, 'shortDate', 'en')
    );
    this.lineChartData = [];
    sensorNames.forEach((name) => {
      let measurementData = { data: Array<number>(), label: name };
      this.dataSource.data
        .filter((m) => m.sensorName == name)
        .forEach((m) => measurementData.data.push(m.value!));
      this.lineChartData.push(measurementData);
    });
  }

  updateAllComplete(i: number, name: string, $event: MatCheckboxChange) {
    this.sensorGroup[i].sensors.forEach((s) => {
      if (s.name == name) s.checked = $event.checked;
    });
    this.allComplete[i] =
      this.sensorGroup[i].sensors != null &&
      this.sensorGroup[i].sensors.every((t) => t.checked);
    this.filterRequest.sensorType = this.getCheckedSensorTypes();
    this.filterRequest.sensorName = this.getCheckedSensorNames();
    this.loadData();
  }

  someComplete(i: number): boolean {
    if (this.sensorGroup[i].sensors == null) {
      return false;
    }
    return (
      this.sensorGroup[i].sensors.filter((t) => t.checked).length > 0 &&
      !this.allComplete[i]
    );
  }

  setAll(i: number, $event: MatCheckboxChange) {
    this.allComplete[i] = $event.checked;
    if (this.sensorGroup[i].sensors == null) {
      return;
    }
    this.sensorGroup[i].checked = $event.checked;
    this.sensorGroup[i].sensors.forEach((t) => (t.checked = $event.checked));
    this.filterRequest.sensorType = this.getCheckedSensorTypes();
    this.filterRequest.sensorName = this.getCheckedSensorNames();
    this.loadData();
  }

  sortColumnChanged($event: Sort) {
    this.sortRequest = {
      columnName: $event.active,
      direction:
        $event.direction == 'asc' ? SortDirection._0 : SortDirection._1,
    };
    this.loadData();
  }

  dateChanged() {
    this.filterRequest.from = this.dateRange.controls.start.value ?? undefined;
    this.filterRequest.to = this.dateRange.controls.end.value ?? undefined;
    this.loadData();
  }

  downloadCSV() {
    // TODO
    this._backendClient
      .measurementsCsv(
        this.filterRequest.from,
        this.filterRequest.to,
        this.filterRequest.sensorType,
        this.filterRequest.sensorName,
        this.sortRequest.columnName,
        this.sortRequest.direction
      )
      .subscribe((fileBlob) => {
        this.downloadFile(fileBlob);
        console.log(fileBlob);
      });
  }

  downloadFile(fileResponse: FileResponse) {
    const blob = new Blob([fileResponse.data], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    window.open(url);
  }

  private getCheckedSensorTypes() {
    return this.sensorGroup.filter((g) => g.checked).map((t) => t.name);
  }

  private getCheckedSensorNames() {
    const checked = this.sensorGroup.map(
      (group) => group.sensors.filter((s) => s.checked).map((s) => s.name) ?? []
    );
    return checked.reduce((acc, curr) => acc.concat(curr), []);
  }

  private loadData() {
    return this._backendClient
      .measurements(
        this.filterRequest.from,
        this.filterRequest.to,
        this.filterRequest.sensorType,
        this.filterRequest.sensorName,
        this.sortRequest.columnName,
        this.sortRequest.direction
      )
      .subscribe((data) => {
        this.dataSource.data = data;
        this.dataSize = data.length;
        this.refreshChart();
      });
  }
}
