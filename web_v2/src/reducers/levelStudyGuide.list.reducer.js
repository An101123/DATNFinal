import {
  GET_LEVELSTUDYGUIDE_LIST,
  GET_LEVELSTUDYGUIDE_LIST_SUCCESS,
  GET_LEVELSTUDYGUIDE_LIST_FAILED,
} from "../actions/levelStudyGuide.list.action";

const initialState = {
  levelStudyGuidePagedList: {},
  loading: false,
  failed: false,
};

export function levelStudyGuideListReducer(state = initialState, action) {
  switch (action.type) {
    case GET_LEVELSTUDYGUIDE_LIST:
      return Object.assign({}, state, {
        loading: true,
        failed: false,
      });
    case GET_LEVELSTUDYGUIDE_LIST_SUCCESS:
      return Object.assign({}, state, {
        levelStudyGuidePagedList: action.payload,
        loading: false,
        failed: false,
      });
    case GET_LEVELSTUDYGUIDE_LIST_FAILED:
      return Object.assign({}, state, {
        loading: false,
        failed: true,
      });
    default:
      return state;
  }
}
