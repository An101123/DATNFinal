export const GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST =
  "[CLASSIFICATIONOFSCIENTIFICWORK_LIST] GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST";
export const GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST_SUCCESS =
  "[CLASSIFICATIONOFSCIENTIFICWORK_LIST] GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST_SUCCESS";
export const GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST_FAILED =
  "[CLASSIFICATIONOFSCIENTIFICWORK_LIST] GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST_FAILED";

export const getClassificationOfScientificWorkList = (params) => {
  return {
    type: GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST,
    payload: {
      params,
    },
  };
};

export const getClassificationOfScientificWorkListSuccess = (params) => {
  return {
    type: GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST_SUCCESS,
    payload: params,
  };
};

export const getClassificationOfScientificWorkListFailed = () => {
  return {
    type: GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST_FAILED,
  };
};