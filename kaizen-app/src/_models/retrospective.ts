import {Participant} from './participant';
import {Like, Dislike, ActionItem} from './retrospectiveitems';

export class RetrospectiveDetails {
  Id: string;
  Owner: string;
  State: string;
  CreatedOn: string;
  Participants: Participant[];
  Likes: Like[];
  Dislikes: Dislike[];
  ActionItems: ActionItem[];
  LikesCount: number;
  DislikesCount: number;
  ActionItemsCount: number;
}


