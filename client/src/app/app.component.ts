import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Fixture, FixtureService } from './fixture.service';

@Component({
  selector: 'app-root', standalone: true, imports: [CommonModule],
  template: `
  <main class="shell">
    <header><div><p class="eyebrow">CHARTERING OPERATIONS</p><h1>PortCall</h1><p class="sub">Fixture management and approval workflow</p></div><button (click)="refresh()">Refresh</button></header>
    <section class="stats"><article><span>Total fixtures</span><strong>{{fixtures.length}}</strong></article><article><span>Awaiting approval</span><strong>{{pendingCount}}</strong></article><article><span>Approved</span><strong>{{approvedCount}}</strong></article></section>
    <section class="panel"><div class="panel-title"><div><h2>Charter fixtures</h2><p class="sub">Review commercial terms and approval status</p></div><span class="tag">WORKFLOW</span></div>
      <div class="table-wrap"><table><thead><tr><th>Reference</th><th>Vessel</th><th>Charterer</th><th>Route</th><th>Freight rate</th><th>Status</th></tr></thead>
      <tbody><tr *ngFor="let f of fixtures"><td class="ref">{{f.reference}}</td><td>{{f.vesselName}}</td><td>{{f.charterer}}</td><td>{{f.loadPort}} → {{f.dischargePort}}</td><td>{{f.freightRate | number:'1.0-0'}} {{f.currency}}</td><td><span class="status" [class.pending]="f.status==='PendingApproval'" [class.approved]="f.status==='Approved'">{{label(f.status)}}</span></td></tr></tbody></table></div>
      <p class="note" *ngIf="usingDemo">Showing fallback demo data because the API is unavailable. Start the .NET API to load service data.</p>
    </section><footer>PortCall demo · Approval actions are exposed through the REST API and Swagger UI.</footer>
  </main>`
})
export class AppComponent implements OnInit {
  fixtures: Fixture[] = [];
  usingDemo = false;
  constructor(private service: FixtureService) {}
  ngOnInit() { this.refresh(); }
  refresh() { this.service.getFixtures().subscribe(items => { this.fixtures = items; this.usingDemo = items.some(x => x.id === 2); }); }
  get pendingCount() { return this.fixtures.filter(f => f.status === 'PendingApproval').length; }
  get approvedCount() { return this.fixtures.filter(f => f.status === 'Approved').length; }
  label(status: string) { return status.replace(/([a-z])([A-Z])/g, '$1 $2'); }
}
