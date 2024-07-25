import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NestedService } from './nested.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-nested',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, CommonModule],
  providers: [NestedService],
  templateUrl: './nested.component.html'
})
export class NestedComponent {

  constructor(public nestedService: NestedService) {}

}
