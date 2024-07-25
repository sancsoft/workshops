import { Component } from '@angular/core';
import { NestedService } from '../nested.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-page-one',
  standalone: true,
  imports: [],
  templateUrl: './page-one.component.html'
})
export class PageOneComponent {
  
  constructor(public nestedService: NestedService) {}

}
