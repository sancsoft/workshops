import { Component } from '@angular/core';
import { NestedService } from '../nested.service';

@Component({
  selector: 'app-page-three',
  standalone: true,
  imports: [],
  templateUrl: './page-three.component.html'
})
export class PageThreeComponent {

  constructor(public nestedService: NestedService) {}

}
