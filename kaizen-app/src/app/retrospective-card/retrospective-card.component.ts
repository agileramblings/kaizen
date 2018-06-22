import { Component, OnInit, Input } from '@angular/core';
import { RetrospectiveDetails } from '../../_models/retrospective';
import { RetrospectiveItem } from '../../_models/retrospectiveitems';
import { RetrospectiveService } from '../../_services/retrospective.service';


@Component({
  selector: 'app-retrospective-card',
  templateUrl: './retrospective-card.component.html',
  styleUrls: ['./retrospective-card.component.css']
})

export class RetrospectiveCardComponent implements OnInit {

  @Input() item: RetrospectiveItem;
  @Input() retrospective: RetrospectiveDetails;
  @Input() type: string;

  constructor(private retroService: RetrospectiveService) { }

  ngOnInit() { }

  upvoteItem(): void {
    alert(`${this.type} upvote item ${this.item.id} from retro ${this.retrospective.id}`);
  }
  downvoteItem(): void {
    alert(`downvote item ${this.item.id} from retro ${this.retrospective.id}`);
  }
  deleteItem(): void {
    alert(`delete item ${this.item.id} from retro ${this.retrospective.id}`);
  }
}
