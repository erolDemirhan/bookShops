export interface bookDTO{
    id: number;
    title: string;
    poster: string;
}

export interface bookCreationDTO{
    title: string;
    inShops: boolean;
    trailer: string;
    releaseDate?: Date;
    poster?: File;
    posterURL?: string;
    genresIds?: number[];
    bookShopsIds?: number[];
    bookShopsIds?: number[];
    authors?: authorBookDTO[];    
}

export interface landingPageDTO {
    inShops?: bookDTO[];
    upcomingBooks?: bookDTO[];
}