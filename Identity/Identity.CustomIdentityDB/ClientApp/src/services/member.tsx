import { baseApi } from "./base";
import { GET_MEMBER_INFO } from "./apis";

export async function getMemberDetailService(memberId: string) : Promise<any> {
    return await baseApi.get(GET_MEMBER_INFO.replace("{{memberId}}", memberId));
}