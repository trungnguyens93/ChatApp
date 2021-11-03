import { baseApi } from "./base";
import { GET_GROUPS } from "./apis";

export async function getGroupsService(memberId: string) : Promise<any> {
    return await baseApi.get(GET_GROUPS.replace("{{memberId}}", memberId));
}