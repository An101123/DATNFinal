export const GET_OTHERSCIENTIFICWORK_LIST =
  "[OTHERSCIENTIFICWORK_LIST] GET_OTHERSCIENTIFICWORK_LIST";
export const GET_OTHERSCIENTIFICWORK_LIST_SUCCESS =
  "[OTHERSCIENTIFICWORK_LIST] GET_OTHERSCIENTIFICWORK_LIST_SUCCESS";
export const GET_OTHERSCIENTIFICWORK_LIST_FAILED =
  "[OTHERSCIENTIFICWORK_LIST] GET_OTHERSCIENTIFICWORK_LIST_FAILED";

export const getOtherScientificWorkList = (params) => {
  return {
    type: GET_OTHERSCIENTIFICWORK_LIST,
    payload: {
      params,
    },
  };
};

export const getOtherScientificWorkListSuccess = (params) => {
  return {
    type: GET_OTHERSCIENTIFICWORK_LIST_SUCCESS,
    payload: params,
  };
};

export const getOtherScientificWorkListFailed = () => {
  return {
    type: GET_OTHERSCIENTIFICWORK_LIST_FAILED,
  };
};
