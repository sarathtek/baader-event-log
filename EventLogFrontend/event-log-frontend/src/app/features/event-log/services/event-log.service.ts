import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Event } from '../models/event';

@Injectable({
  providedIn: 'root'
})
export class EventLogService {

  private baseUrl = 'https://localhost:7295/api/events';

  constructor(private http: HttpClient) {}

  getEventsSince(from: Date): Observable<Event[]> {
    const params = new HttpParams().set('from', from.toISOString());
    return this.http.get<Event[]>(this.baseUrl, { params });
  }
}
