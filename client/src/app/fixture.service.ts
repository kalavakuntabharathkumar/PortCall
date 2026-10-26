import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../environments/environment';

export interface Fixture {
  id: number; reference: string; vesselName: string; charterer: string; loadPort: string;
  dischargePort: string; freightRate: number; currency: string; status: string; decisionNote?: string;
}
const demoFixtures: Fixture[] = [
  { id: 1, reference: 'FIX-2026-001', vesselName: 'MV Ocean Pioneer', charterer: 'Bluewater Chartering',
    loadPort: 'Singapore', dischargePort: 'Rotterdam', freightRate: 18500, currency: 'USD', status: 'PendingApproval' },
  { id: 2, reference: 'FIX-2026-002', vesselName: 'MV Meridian Star', charterer: 'North Sea Logistics',
    loadPort: 'Mumbai', dischargePort: 'Jebel Ali', freightRate: 9200, currency: 'USD', status: 'Approved' }
];
@Injectable({ providedIn: 'root' })
export class FixtureService {
  constructor(private http: HttpClient) {}
  getFixtures(): Observable<Fixture[]> {
    return this.http.get<Fixture[]>(`${environment.apiUrl}/fixtures`).pipe(catchError(() => of(demoFixtures)));
  }
}
