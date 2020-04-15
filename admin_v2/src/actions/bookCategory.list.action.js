export const GET_BOOKCATEGORY_LIST = "[BOOKCATEGORY_LIST] GET_BOOKCATEGORY_LIST";
export const GET_BOOKCATEGORY_LIST_SUCCESS =
  "[BOOKCATEGORY_LIST] GET_BOOKCATEGORY_LIST_SUCCESS";
export const GET_BOOKCATEGORY_LIST_FAILED =
  "[BOOKCATEGORY_LIST] GET_BOOKCATEGORY_LIST_FAILED";

export const getBookCategoryList = (params) =>{
    return {
        type : GET_BOOKCATEGORY_LIST,
        payload : {
            params
        }
    }
}

export const getBookCategoryListSuccess = params =>{
    return {
        type : GET_BOOKCATEGORY_LIST_SUCCESS,
        payload : params
    }
}

export const getBookCategoryListFailed = () =>{
    return {
        type : GET_BOOKCATEGORY_LIST_FAILED
    }
}