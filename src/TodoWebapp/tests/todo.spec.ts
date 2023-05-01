import { test, expect } from '@playwright/test';

test('Check Page Load', async ({ page }) => {
  await page.goto('/');

  await expect(page).toHaveTitle('List of To-Do Items');

  const locator = page.locator('#mainTitle');
  await expect(locator).toHaveText('To-Dos');
});
