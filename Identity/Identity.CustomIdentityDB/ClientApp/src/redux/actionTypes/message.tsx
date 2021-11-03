export const GET_MESSAGES = "messages/GET_MESSAGES";
export interface GetMessagesAction {
  type: typeof GET_MESSAGES,
  groupId: string
}

export const GET_MESSAGES_SUCCESS = "messages/GET_MESSAGES_SUCCESS";
export interface GetMessagesSuccessAction {
  type: typeof GET_MESSAGES_SUCCESS,
  messages: Array<{
    messageId: string,
    content: string,
    sender: string
  }>
}

export const GET_MESSAGES_FAIL = "messages/GET_MESSAGES_FAIL";
export interface GetMessagesFailAction {
  type: typeof GET_MESSAGES_FAIL;
  error: any;
}

export type MessageAction = 
  | GetMessagesAction
  | GetMessagesSuccessAction
  | GetMessagesFailAction;
