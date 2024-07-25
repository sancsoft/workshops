import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AppService } from '../app.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
})
export class HomeComponent {

  constructor(public appService: AppService) {}

}
