import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class OccupationsService {
    private baseUrl: string = environment.baseUrl + 'api/';

    constructor(private http: HttpClient) { }    

    public retrieveOccupations() {
         const headers= new HttpHeaders()
                        .set('content-type', 'application/json')
                        .set('Access-Control-Allow-Origin', '*');
        return this.http.get(this.baseUrl + 'occupations/', { 'headers': headers });
    }
}