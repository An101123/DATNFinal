export const GET_LEVELSTUDYGUIDE_LIST =
  "[LEVELSTUDYGUIDE_LIST] GET_LEVELSTUDYGUIDE_LIST";
export const GET_LEVELSTUDYGUIDE_LIST_SUCCESS =
  "[LEVELSTUDYGUIDE_LIST] GET_LEVELSTUDYGUIDE_LIST_SUCCESS";
export const GET_LEVELSTUDYGUIDE_LIST_FAILED =
  "[LEVELSTUDYGUIDE_LIST] GET_LEVELSTUDYGUIDE_LIST_FAILED";

export const getLevelStudyGuideList = (params) => {
  return {
    type: GET_LEVELSTUDYGUIDE_LIST,
    payload: {
      params,
    },
  };
};

export const getLevelStudyGuideListSuccess = (params) => {
  return {
    type: GET_LEVELSTUDYGUIDE_LIST_SUCCESS,
    payload: params,
  };
};

export const getLevelStudyGuideListFailed = () => {
  return {
    type: GET_LEVELSTUDYGUIDE_LIST_FAILED,
  };
};
