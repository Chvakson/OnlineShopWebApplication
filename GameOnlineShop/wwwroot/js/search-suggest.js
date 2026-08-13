document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('catalogSearchForm');
    const input = document.getElementById('catalogSearchInput');
    const list = document.getElementById('catalogSearchSuggest');
    if (!form || !input || !list) {
        return;
    }

    let timer = null;
    let items = [];
    let activeIndex = -1;

    function escapeHtml(value) {
        return String(value)
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;');
    }

    function hide() {
        list.hidden = true;
        list.innerHTML = '';
        items = [];
        activeIndex = -1;
    }

    function applySuggestion(text) {
        input.value = text;
        hide();
        form.submit();
    }

    function render() {
        if (!items.length) {
            hide();
            return;
        }

        list.innerHTML = items.map((item, index) => `
            <li>
                <button type="button" class="search-suggest__item${index === activeIndex ? ' is-active' : ''}" data-index="${index}">
                    <span class="search-suggest__text">${escapeHtml(item.text)}</span>
                    <span class="search-suggest__kind">${escapeHtml(item.kind)}</span>
                </button>
            </li>
        `).join('');
        list.hidden = false;
    }

    function fetchSuggestions(term) {
        if (term.length < 1) {
            hide();
            return;
        }

        fetch('/Home/Suggest?q=' + encodeURIComponent(term), { headers: { 'Accept': 'application/json' } })
            .then(response => response.ok ? response.json() : [])
            .then(data => {
                if (input.value.trim() !== term) {
                    return;
                }
                items = Array.isArray(data) ? data : [];
                activeIndex = -1;
                render();
            })
            .catch(hide);
    }

    input.addEventListener('input', function () {
        const term = input.value.trim();
        window.clearTimeout(timer);
        timer = window.setTimeout(function () {
            fetchSuggestions(term);
        }, 180);
    });

    input.addEventListener('keydown', function (event) {
        if (list.hidden || !items.length) {
            return;
        }

        if (event.key === 'ArrowDown') {
            event.preventDefault();
            activeIndex = (activeIndex + 1) % items.length;
            render();
        } else if (event.key === 'ArrowUp') {
            event.preventDefault();
            activeIndex = (activeIndex - 1 + items.length) % items.length;
            render();
        } else if (event.key === 'Enter' && activeIndex >= 0) {
            event.preventDefault();
            applySuggestion(items[activeIndex].text);
        } else if (event.key === 'Escape') {
            hide();
        }
    });

    list.addEventListener('mousedown', function (event) {
        const button = event.target.closest('[data-index]');
        if (!button) {
            return;
        }
        event.preventDefault();
        const index = Number(button.dataset.index);
        if (items[index]) {
            applySuggestion(items[index].text);
        }
    });

    input.addEventListener('blur', function () {
        window.setTimeout(hide, 120);
    });

    input.addEventListener('focus', function () {
        const term = input.value.trim();
        if (term.length >= 1) {
            fetchSuggestions(term);
        }
    });
});
