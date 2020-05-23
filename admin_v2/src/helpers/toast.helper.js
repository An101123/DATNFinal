import { toast } from "react-toastify";
const options = {
  autoClose: 3000,
  hideProgressBar: true,
};

export const toastSuccess = (message) => {
  toast.info(message.toString(), options);
};

export const toastError = (message) => {
  toast.error(message.toString(), options);
};

export const toastWarning = (message) => {
  toast.warn(message.toString(), options);
};
