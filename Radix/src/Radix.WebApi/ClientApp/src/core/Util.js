export const ConvertTimeStampToDateTime = (timeStamp) => {
    const date = new Date(timeStamp * 1000);

    const day = date.getDate() < 10 ? `0${date.getDate()}` : date.getDate();
    const month =
        date.getMonth() < 10 ? `0${date.getMonth() + 1}` : date.getMonth() + 1;
    const year = date.getFullYear();

    const hour = date.getHours() < 10 ? `0${date.getHours()}` : date.getHours();
    const minutes =
        date.getMinutes() < 10 ? `0${date.getMinutes()}` : date.getMinutes();

    return `${day}/${month}/${year} - ${hour}:${minutes}`;
};

export const OnlyUnique = (value, index, self) => {
    return self.indexOf(value) === index;
};
