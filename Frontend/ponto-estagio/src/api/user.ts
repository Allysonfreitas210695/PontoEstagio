import { api } from "./api";

type userType = {
    Name: string;
    Type: string;
    AccessToken: string,
    RefreshToken: string
}

type Users = {
    ame: string;
    Type: string;
    AccessToken: string,
    RefreshToken: string
}

// export const userApi = {
//     async getList(){
//       try {
//         const users: userType[] = await api.get(`Users`);

//       return users;
//       } catch (error) {
//         console.log(error)
//       }
//     },
//     async get(){
//         try {
//           const users: Users[] = await api.get(`Users`);
  
//         return users;
//         } catch (error) {
//           console.log(error)
//         }
//       }
// }