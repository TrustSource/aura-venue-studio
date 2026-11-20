import debounce from 'lodash/debounce';
import groupBy from 'lodash/groupBy';
import sortBy from 'lodash/sortBy';

// Only these three lodash functions are used in the front end.
// In particular, _.template is not imported anywhere.

export function amplifiersByZone(amplifiers) {
  return groupBy(sortBy(amplifiers, 'name'), 'zone');
}

export function onEdit(save) {
  return debounce(save, 500);
}
