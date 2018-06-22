import { RetrospectiveDetails } from '../_models/retrospective';

export const RETROSPECTIVE: RetrospectiveDetails = {
    id: '12345678-4444-2222-3333-123451234512',
    owner: 'dave.white@gettyimages.com',
    state: 'CollectionSuggestions',
    createdOn: 'Some date',
    participants: [
        'DavidChen',
        'DaveWhite',
        'TuanNguyen'
    ],
    likes: [
        {id: '1', description: 'some description for like 1', votes: 0},
        {id: '2', description: 'some description for like 2', votes: 0},
        {id: '3', description: 'some description for like 3', votes: 0},
        {id: '4', description: 'some description for like 4', votes: 0},
    ],
    dislikes: [
        {id: '1', description: 'some description for dislike 1', votes: 0},
        {id: '2', description: 'some description for dislike 2', votes: 0},
        {id: '3', description: 'some description for dislike 3', votes: 0},
        {id: '4', description: 'some description for dislike 4', votes: 0},
    ],
    actionItems: [],
    likesCount: 4,
    dislikesCount: 4,
    actionItemsCount: 0,
};

export const RETROSPECTIVE2: RetrospectiveDetails = {
    id: '22222222-4444-2222-3333-123451234512',
    owner: 'david.chen@gettyimages.com',
    state: 'CollectionSuggestions',
    createdOn: 'Some date',
    participants: [
        'DavidChen',
        'DaveWhite',
        'TuanNguyen'
    ],
    likes: [
        {id: '1', description: 'some description for like 1', votes: 0},
        {id: '2', description: 'some description for like 2', votes: 0},
        {id: '3', description: 'some description for like 3', votes: 0},
        {id: '4', description: 'some description for like 4', votes: 0},
    ],
    dislikes: [
        {id: '1', description: 'some description for dislike 1', votes: 0},
        {id: '2', description: 'some description for dislike 2', votes: 0},
        {id: '3', description: 'some description for dislike 3', votes: 0},
        {id: '4', description: 'some description for dislike 4', votes: 0},
    ],
    actionItems: [],
    likesCount: 4,
    dislikesCount: 4,
    actionItemsCount: 0,
};

export const RETROSPECTIVE3: RetrospectiveDetails = {
    id: '33333333-4444-2222-3333-123451234512',
    owner: 'tuan.nguyen@gettyimages.com',
    state: 'CollectionSuggestions',
    createdOn: 'Some date',
    participants: [
        'DavidChen',
        'DaveWhite',
        'TuanNguyen'
    ],
    likes: [
        {id: '1', description: 'some description for like 1', votes: 0},
        {id: '2', description: 'some description for like 2', votes: 0},
        {id: '3', description: 'some description for like 3', votes: 0},
        {id: '4', description: 'some description for like 4', votes: 0},
    ],
    dislikes: [
        {id: '1', description: 'some description for dislike 1', votes: 0},
        {id: '2', description: 'some description for dislike 2', votes: 0},
        {id: '3', description: 'some description for dislike 3', votes: 0},
        {id: '4', description: 'some description for dislike 4', votes: 0},
    ],
    actionItems: [],
    likesCount: 4,
    dislikesCount: 4,
    actionItemsCount: 0,
};

export const RETROSPECTIVES: RetrospectiveDetails[] = [
    RETROSPECTIVE,
    RETROSPECTIVE2,
    RETROSPECTIVE3
];
