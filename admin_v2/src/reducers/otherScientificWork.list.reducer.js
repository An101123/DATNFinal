import {
  GET_OTHERSCIENTIFICWORK_LIST,
  GET_OTHERSCIENTIFICWORK_LIST_SUCCESS,
  GET_OTHERSCIENTIFICWORK_LIST_FAILED,
} from "../actions/otherScientificWork.list.action";

const initialState = {
  otherScientificWorkPagedList: {},
  loading: false,
  failed: false,
};

export function otherScientificWorkListReducer(state = initialState, action) {
  switch (action.type) {
    case GET_OTHERSCIENTIFICWORK_LIST:
      return Object.assign({}, state, {
        loading: true,
        failed: false,
      });
    case GET_OTHERSCIENTIFICWORK_LIST_SUCCESS:
      return Object.assign({}, state, {
        otherScientificWorkPagedList: action.payload,
        loading: false,
        failed: false,
      });
    case GET_OTHERSCIENTIFICWORK_LIST_FAILED:
      return Object.assign({}, state, {
        loading: false,
        failed: true,
      });
    default:
      return state;
  }
}
