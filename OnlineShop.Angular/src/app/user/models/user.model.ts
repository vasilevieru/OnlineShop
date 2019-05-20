export class User {
    id: number;
    isAuthenticated = false;
    accessToken: string;
    refreshToken: string;
    avatar: File;
}
