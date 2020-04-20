import {
  GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST,
  GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST_SUCCESS,
  GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST_FAILED,
} from "../actions/classificationOfScientificWork.list.action";

const initialState = {
  classificationOfScientificWorkPagedList: {},
  loading: false,
  failed: false,
};

export function classificationOfScientificWorkListReducer(
  state = initialState,
  action
) {
  switch (action.type) {
    case GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST:
      return Object.assign({}, state, {
        loading: true,
        failed: false,
      });
    case GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST_SUCCESS:
      return Object.assign({}, state, {
        classificationOfScientificWorkPagedList: action.payload,
        loading: false,
        failed: false,
      });
    case GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST_FAILED:
      return Object.assign({}, state, {
        loading: false,
        failed: true,
      });
    default:
      return state;
  }
}
