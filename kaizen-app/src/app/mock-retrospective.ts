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

export const RETROSPECTIVE2: RetrospectiveDetails = {
    Id: '22222222-4444-2222-3333-123451234512',
    Owner: 'david.chen@gettyimages.com',
    State: 'CollectionSuggestions',
    CreatedOn: 'Some date',
    Participants: [
        {Name: 'David Chen', EmailAddress: 'david.chen@gettyimages.com'},
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

export const RETROSPECTIVE3: RetrospectiveDetails = {
    Id: '33333333-4444-2222-3333-123451234512',
    Owner: 'tuan.nguyen@gettyimages.com',
    State: 'CollectionSuggestions',
    CreatedOn: 'Some date',
    Participants: [
        {Name: 'Tuan Nguyen', EmailAddress: 'tuan.nguyen@gettyimages.com'},
        {Name: 'David Chen', EmailAddress: 'david.chen@gettyimages.com'},
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

export const RETROSPECTIVES: RetrospectiveDetails[] = [
    RETROSPECTIVE,
    RETROSPECTIVE2,
    RETROSPECTIVE3
];
