import {
    GET_BOOKCATEGORY_LIST,
    GET_BOOKCATEGORY_LIST_SUCCESS,
    GET_BOOKCATEGORY_LIST_FAILED
  } from "../actions/bookCategory.list.action";
  
  const initialState = {
    bookCategoryPagedList: {},
    loading: false,
    failed: false
  };
  
  export function bookCategoryListReducer(state = initialState, action) {
    switch (action.type) {
      case GET_BOOKCATEGORY_LIST:
        return Object.assign({}, state, {
          loading: true,
          failed: false
        });
      case GET_BOOKCATEGORY_LIST_SUCCESS:
        return Object.assign({}, state, {
          bookCategoryPagedList: action.payload,
          loading: false,
          failed: false
        });
      case GET_BOOKCATEGORY_LIST_FAILED:
        return Object.assign({}, state, {
          loading: false,
          failed: true
        });
      default:
        return state;
    }
  }