//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, OpaqueToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new OpaqueToken('API_BASE_URL');

@Injectable()
export class Client {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    /**
     * @param from (optional) 
     * @param to (optional) 
     * @param sensorType (optional) 
     * @param sensorName (optional) 
     * @param columnName (optional) 
     * @param direction (optional) 
     * @return Success
     */
    sensors(from: Date | undefined, to: Date | undefined, sensorType: string | undefined, sensorName: string | undefined, columnName: string | undefined, direction: SortDirection | undefined): Observable<SensorMeasurementDto[]> {
        let url_ = this.baseUrl + "/api/sensors?";
        if (from === null)
            throw new Error("The parameter 'from' cannot be null.");
        else if (from !== undefined)
            url_ += "From=" + encodeURIComponent(from ? "" + from.toISOString() : "") + "&";
        if (to === null)
            throw new Error("The parameter 'to' cannot be null.");
        else if (to !== undefined)
            url_ += "To=" + encodeURIComponent(to ? "" + to.toISOString() : "") + "&";
        if (sensorType === null)
            throw new Error("The parameter 'sensorType' cannot be null.");
        else if (sensorType !== undefined)
            url_ += "SensorType=" + encodeURIComponent("" + sensorType) + "&";
        if (sensorName === null)
            throw new Error("The parameter 'sensorName' cannot be null.");
        else if (sensorName !== undefined)
            url_ += "SensorName=" + encodeURIComponent("" + sensorName) + "&";
        if (columnName === null)
            throw new Error("The parameter 'columnName' cannot be null.");
        else if (columnName !== undefined)
            url_ += "ColumnName=" + encodeURIComponent("" + columnName) + "&";
        if (direction === null)
            throw new Error("The parameter 'direction' cannot be null.");
        else if (direction !== undefined)
            url_ += "Direction=" + encodeURIComponent("" + direction) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "text/plain"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processSensors(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processSensors(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<SensorMeasurementDto[]>;
                }
            } else
                return _observableThrow(response_) as any as Observable<SensorMeasurementDto[]>;
        }));
    }

    protected processSensors(response: HttpResponseBase): Observable<SensorMeasurementDto[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(SensorMeasurementDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<SensorMeasurementDto[]>(null as any);
    }

    /**
     * @return Success
     */
    getWeatherForecast(): Observable<WeatherForecast[]> {
        let url_ = this.baseUrl + "/WeatherForecast";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "text/plain"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetWeatherForecast(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetWeatherForecast(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<WeatherForecast[]>;
                }
            } else
                return _observableThrow(response_) as any as Observable<WeatherForecast[]>;
        }));
    }

    protected processGetWeatherForecast(response: HttpResponseBase): Observable<WeatherForecast[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(WeatherForecast.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<WeatherForecast[]>(null as any);
    }
}

export class SensorMeasurementDto implements ISensorMeasurementDto {
    sensorType?: string | undefined;
    sensorName?: string | undefined;
    value?: number;
    unit?: string | undefined;
    measurementDate?: Date;

    constructor(data?: ISensorMeasurementDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.sensorType = _data["sensorType"];
            this.sensorName = _data["sensorName"];
            this.value = _data["value"];
            this.unit = _data["unit"];
            this.measurementDate = _data["measurementDate"] ? new Date(_data["measurementDate"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): SensorMeasurementDto {
        data = typeof data === 'object' ? data : {};
        let result = new SensorMeasurementDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["sensorType"] = this.sensorType;
        data["sensorName"] = this.sensorName;
        data["value"] = this.value;
        data["unit"] = this.unit;
        data["measurementDate"] = this.measurementDate ? this.measurementDate.toISOString() : <any>undefined;
        return data;
    }
}

export interface ISensorMeasurementDto {
    sensorType?: string | undefined;
    sensorName?: string | undefined;
    value?: number;
    unit?: string | undefined;
    measurementDate?: Date;
}

export enum SortDirection {
    _0 = 0,
    _1 = 1,
}

export class WeatherForecast implements IWeatherForecast {
    date?: Date;
    temperatureC?: number;
    readonly temperatureF?: number;
    summary?: string | undefined;

    constructor(data?: IWeatherForecast) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.date = _data["date"] ? new Date(_data["date"].toString()) : <any>undefined;
            this.temperatureC = _data["temperatureC"];
            (<any>this).temperatureF = _data["temperatureF"];
            this.summary = _data["summary"];
        }
    }

    static fromJS(data: any): WeatherForecast {
        data = typeof data === 'object' ? data : {};
        let result = new WeatherForecast();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["date"] = this.date ? this.date.toISOString() : <any>undefined;
        data["temperatureC"] = this.temperatureC;
        data["temperatureF"] = this.temperatureF;
        data["summary"] = this.summary;
        return data;
    }
}

export interface IWeatherForecast {
    date?: Date;
    temperatureC?: number;
    temperatureF?: number;
    summary?: string | undefined;
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new ApiException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((event.target as any).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}