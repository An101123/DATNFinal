export const GET_PUBLISHBOOK_LIST = "[PUBLISHBOOK_LIST] GET_PUBLISHBOOK_LIST";
export const GET_PUBLISHBOOK_LIST_SUCCESS =
  "[PUBLISHBOOK_LIST] GET_PUBLISHBOOK_LIST_SUCCESS";
export const GET_PUBLISHBOOK_LIST_FAILED =
  "[PUBLISHBOOK_LIST] GET_PUBLISHBOOK_LIST_FAILED";

export const getPublishBookList = (params) =>{
    return {
        type : GET_PUBLISHBOOK_LIST,
        payload : {
            params
        }
    }
}

export const getPublishBookListSuccess = params =>{
    return {
        type : GET_PUBLISHBOOK_LIST_SUCCESS,
        payload : params
    }
}

export const getPublishBookListFailed = () =>{
    return {
        type : GET_PUBLISHBOOK_LIST_FAILED
    }
}