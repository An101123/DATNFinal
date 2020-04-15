import {
  GET_PUBLISHBOOK_LIST,
  GET_PUBLISHBOOK_LIST_SUCCESS,
  GET_PUBLISHBOOK_LIST_FAILED,
} from "../actions/publishBook.list.action";

const initialState = {
  publishBookPagedList: {},
  loading: false,
  failed: false,
};

export function publishBookListReducer(state = initialState, action) {
  switch (action.type) {
    case GET_PUBLISHBOOK_LIST:
      return Object.assign({}, state, {
        loading: true,
        failed: false,
      });
    case GET_PUBLISHBOOK_LIST_SUCCESS:
      return Object.assign({}, state, {
        publishBookPagedList: action.payload,
        loading: false,
        failed: false,
      });
    case GET_PUBLISHBOOK_LIST_FAILED:
      return Object.assign({}, state, {
        loading: false,
        failed: true,
      });
    default:
      return state;
  }
}
