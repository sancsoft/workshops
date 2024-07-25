import { Component } from '@angular/core';
import { NestedService } from '../nested.service';

@Component({
  selector: 'app-page-two',
  standalone: true,
  imports: [],
  templateUrl: './page-two.component.html'
})
export class PageTwoComponent {

  constructor(public nestedService: NestedService) {}

}
