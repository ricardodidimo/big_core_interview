import type { OdometerFilterParamsInput } from "../api/odometer";
import type { OdometerTrackerTableFields } from "../components/odometerTrackerTable/ConfigureVisualizationModal.vue";

const THEME_KEY = 'user-theme';
const LANGUAGE_KEY = 'user-language';
const TABLE_PREFS_KEY = 'config-preferences';
const FILTERS_KEY = 'user-filters';

export function getTheme(): string {
  return localStorage.getItem(THEME_KEY) || 'dark';
}

export function setTheme(theme: string): void {
  localStorage.setItem(THEME_KEY, theme);
}

export function getLanguage(): string {
  return localStorage.getItem(LANGUAGE_KEY) || 'pt';
}

export function setLanguage(language: string): void {
  localStorage.setItem(LANGUAGE_KEY, language);
}

export function getTablePreferences(): OdometerTrackerTableFields[]|null {
  const prefs = localStorage.getItem(TABLE_PREFS_KEY);
  if (prefs === null) { return null }
  var obj: OdometerTrackerTableFields[] = JSON.parse(prefs);
  return obj;
}

export function setTablePreferences(preferences: OdometerTrackerTableFields[]): void {
  localStorage.setItem(TABLE_PREFS_KEY, JSON.stringify(preferences));
}

export function getFilters(): OdometerFilterParamsInput|null {
  const filters = localStorage.getItem(FILTERS_KEY);
  return filters ? JSON.parse(filters) : null;
}

export function setFilters(filters: OdometerFilterParamsInput): void {
  localStorage.setItem(FILTERS_KEY, JSON.stringify(filters));
}

export function clearAll(): void {
  localStorage.removeItem(THEME_KEY);
  localStorage.removeItem(LANGUAGE_KEY);
  localStorage.removeItem(TABLE_PREFS_KEY);
  localStorage.removeItem(FILTERS_KEY);
}
