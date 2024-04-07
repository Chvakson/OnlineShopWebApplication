function GetValidDateTimeValue(DateTimeValue) {
    if (DateTimeValue < 10)
        return "0" + DateTimeValue;
    return DateTimeValue.toString();
}