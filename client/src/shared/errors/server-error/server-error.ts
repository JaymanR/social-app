import { Component, computed, effect, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { ApiError } from '../../../types/error';

@Component({
  selector: 'app-server-error',
  imports: [],
  templateUrl: './server-error.html',
  styleUrl: './server-error.css',
})
export class ServerError {
  protected error: ApiError;
  private router = inject(Router);
  protected showDetails = false;

  private isNavigationActive = computed(() => !!this.router.currentNavigation());

  constructor() {
    const nav = this.router.currentNavigation();
    this.error = nav?.extras?.state?.["error"];
  }

  detailsToggle() {
    this.showDetails = !this.showDetails;
  }
}
