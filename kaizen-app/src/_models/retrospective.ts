import {Participant} from './participant';
import {Like, Dislike, ActionItem} from './retrospectiveitems';

export class RetrospectiveDetails {
  id: string;
  owner: string;
  state: string;
  createdOn: string;
  participants: string[];
  likes: Like[];
  dislikes: Dislike[];
  actionItems: ActionItem[];
  likesCount: number;
  dislikesCount: number;
  actionItemsCount: number;
}
