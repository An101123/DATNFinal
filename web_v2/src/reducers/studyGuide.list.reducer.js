import {
  GET_STUDYGUIDE_LIST,
  GET_STUDYGUIDE_LIST_SUCCESS,
  GET_STUDYGUIDE_LIST_FAILED,
} from "../actions/studyGuide.list.action";

const initialState = {
  studyGuidePagedList: {},
  loading: false,
  failed: false,
};

export function studyGuideListReducer(state = initialState, action) {
  switch (action.type) {
    case GET_STUDYGUIDE_LIST:
      return Object.assign({}, state, {
        loading: true,
        failed: false,
      });
    case GET_STUDYGUIDE_LIST_SUCCESS:
      return Object.assign({}, state, {
        studyGuidePagedList: action.payload,
        loading: false,
        failed: false,
      });
    case GET_STUDYGUIDE_LIST_FAILED:
      return Object.assign({}, state, {
        loading: false,
        failed: true,
      });
    default:
      return state;
  }
}
