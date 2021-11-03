import { baseApi } from "./base";
import { GET_MESSAGES } from "./apis";

export async function getMessagesService(groupId: string) : Promise<any> {
    return await baseApi.get(GET_MESSAGES.replace("{{groupId}}", groupId));
}