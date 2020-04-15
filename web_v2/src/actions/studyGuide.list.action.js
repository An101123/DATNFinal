export const GET_STUDYGUIDE_LIST = "[STUDYGUIDE_LIST] GET_STUDYGUIDE_LIST";
export const GET_STUDYGUIDE_LIST_SUCCESS =
  "[STUDYGUIDE_LIST] GET_STUDYGUIDE_LIST_SUCCESS";
export const GET_STUDYGUIDE_LIST_FAILED =
  "[STUDYGUIDE_LIST] GET_STUDYGUIDE_LIST_FAILED";

export const getStudyGuideList = (params) => {
  return {
    type: GET_STUDYGUIDE_LIST,
    payload: {
      params,
    },
  };
};

export const getStudyGuideListSuccess = (params) => {
  return {
    type: GET_STUDYGUIDE_LIST_SUCCESS,
    payload: params,
  };
};

export const getStudyGuideListFailed = () => {
  return {
    type: GET_STUDYGUIDE_LIST_FAILED,
  };
};
