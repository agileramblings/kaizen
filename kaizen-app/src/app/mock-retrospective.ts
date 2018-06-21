import { RetrospectiveDetails } from '../_models/retrospective';

export const RETROSPECTIVE: RetrospectiveDetails = {
    Id: '12345678-4444-2222-3333-123451234512',
    Owner: 'dave.white@gettyimages.com',
    State: 'CollectionSuggestions',
    CreatedOn: 'Some date',
    Participants: [
        {Name: 'Dave White', EmailAddress: 'dave.white@gettyimages.com'}
    ],
    Likes: [
        {Id: '1', Description: 'some description for like 1', Votes: 0},
        {Id: '2', Description: 'some description for like 2', Votes: 0},
        {Id: '3', Description: 'some description for like 3', Votes: 0},
        {Id: '4', Description: 'some description for like 4', Votes: 0},
    ],
    Dislikes: [
        {Id: '1', Description: 'some description for dislike 1', Votes: 0},
        {Id: '2', Description: 'some description for dislike 2', Votes: 0},
        {Id: '3', Description: 'some description for dislike 3', Votes: 0},
        {Id: '4', Description: 'some description for dislike 4', Votes: 0},
    ],
    ActionItems: [],
    LikesCount: 4,
    DislikesCount: 4,
    ActionItemsCount: 0,
};
