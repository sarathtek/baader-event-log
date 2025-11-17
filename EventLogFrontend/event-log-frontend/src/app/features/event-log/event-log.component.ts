import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventLogService } from './services/event-log.service';
import { Event } from './models/event';

@Component({
  selector: 'app-event-log',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './event-log.component.html',
  styleUrl: './event-log.component.css'
})
export class EventLogComponent implements OnInit {

  title = 'Event Log';
  events: Event[] = [];
  loading = false;

  constructor(private eventService: EventLogService) {}

  ngOnInit(): void {
    const now = new Date();
    const fiveMinutesAgo = new Date(now.getTime() - 5 * 60 * 1000);

    this.loading = true;
    this.eventService.getEventsSince(fiveMinutesAgo).subscribe({
      next: data => {
        this.events = data;
        this.loading = false;
      },
      error: err => {
        console.error(err);
        this.loading = false;
      }
    });
  }
}